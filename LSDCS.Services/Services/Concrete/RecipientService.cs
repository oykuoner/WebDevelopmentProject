using AutoMapper;
using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.DTOs.MailRecipient;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LSDCS.Service.Services.Concrete
{
    public class RecipientService  : IRecipientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        

        public RecipientService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }



        public async Task<(List<int> recipientsListIdTO, List<int> recipientsListIdCC)> ProcessEmailListsAsync(string ALICI_TO, string ALICI_CC)
        {

            var recipientsListTO = ALICI_TO.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(email => email.Trim().ToLowerInvariant()).ToList();

            var recipientsListCC = string.IsNullOrEmpty(ALICI_CC) ? new List<string>()
                         : ALICI_CC.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(email => email.Trim().ToLowerInvariant()).ToList();


            var recipientsListIdTO = new List<int>();
            var recipientsListIdCC = new List<int>();

            // TO ve CC listelerindeki e-posta adreslerinin birleşimini al
            var allEmails = recipientsListTO.Concat(recipientsListCC).Distinct().ToList();

            // Veritabanında zaten mevcut olan ve yeni eklenmesi gereken e-postalar için kontrol yap
            var existingRecipients = await _uow.GetRepository<Recipients>()
                                                .GetAllAsync(r => allEmails.Contains(r.ALICI_MAIL));

            // Mevcut olan e-posta adreslerinin listesini al
            var existingEmails = existingRecipients.Select(r => r.ALICI_MAIL).ToList();

            foreach (var email in allEmails)
            {
                var existingRecipient = existingRecipients.FirstOrDefault(r => r.ALICI_MAIL == email);
                if (existingRecipient == null)
                {
                    // Eğer e-posta adresi mevcut değilse yeni bir Recipient ekleyin
                    var newRecipient = new Recipients { ALICI_MAIL = email.ToLowerInvariant().Trim() };
                    await _uow.GetRepository<Recipients>().AddAsync(newRecipient);
                    existingRecipients.Add(newRecipient); // Yeni eklenen recipient'i listeye ekleyin
                    existingEmails.Add(email); // Yeni eklenen e-posta adresini listeye ekleyin
                }
            }

            // Tüm değişiklikleri veritabanına tek bir seferde uygula
            await _uow.SaveAsync();

            // TO ve CC listelerindeki her bir e-posta için ID'leri al
            foreach (var email in recipientsListTO.Distinct())
            {
                if (existingEmails.Contains(email))
                {
                    var recipientId = existingRecipients.First(r => r.ALICI_MAIL == email).Id;
                    recipientsListIdTO.Add(recipientId);
                }
            }

            foreach (var email in recipientsListCC.Distinct())
            {
                if (existingEmails.Contains(email))
                {
                    var recipientId = existingRecipients.First(r => r.ALICI_MAIL == email).Id;
                    recipientsListIdCC.Add(recipientId);
                }
            }

            return (recipientsListIdTO, recipientsListIdCC);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var Idn = new IdnMapping();
                    string domainName = Idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }


    }
}
