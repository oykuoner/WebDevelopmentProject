using LSDCS.DataAccess.UnitOfWorks;
using LSDCS.Entity.Entities;
using LSDCS.Service.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LSDCS.Service.Services.Concrete
{

    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _environment;
        private string _documentsPath;
        public DocumentService(IUnitOfWork uow, IWebHostEnvironment environment)
        {
            _uow = uow;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _documentsPath = Path.Combine(_environment.WebRootPath, "BTSDocuments");
        }

        public async Task<List<int>> DocumentAdd(List<IFormFile> documentFiles)
        {
            List<string> normalizedFileNames = new List<string>();
            List<string> hashedFileNames = new List<string>();
            List<int> documentIds = new List<int>();
            foreach (var file in documentFiles)
            {
                string normalizedFileName = NormalizeFileName(file.FileName);
                // Normalize edilmiş dosya adını hash'le ve uzantısını ekle
                string hashedFileName = HashFileName(normalizedFileName) + Path.GetExtension(file.FileName);

                // Hash'lenmiş dosya adını listeye ekle
                hashedFileNames.Add(hashedFileName);
                // Normalize edilmiş dosya adını da bir liste ekleyelim
                normalizedFileNames.Add(normalizedFileName);

                var existingDocument = await _uow.GetRepository<Documents>().GetAllAsync(d => d.DOKUMAN_ADI_GUID == hashedFileName);
                if (!existingDocument.Any())
                {
                    var document = new Documents
                    {
                        DOKUMAN_ADI = normalizedFileName,
                        DOKUMAN_ADI_GUID = hashedFileName
                    };

                    // Dosyayı kaydet, burada SaveFile metoduna hash'lenmiş dosya adını veriyoruz
                    await SaveFile(file, hashedFileName);
                    await _uow.GetRepository<Documents>().AddAsync(document);
                }
            }

            await _uow.SaveAsync();
            foreach (var documentName in hashedFileNames.Distinct())
            {
                var document = await _uow.GetRepository<Documents>().GetAsync(d => d.DOKUMAN_ADI_GUID == documentName);
                if (document != null)
                {
                    var documentID = document.Id;
                    documentIds.Add(documentID);
                }
            }


            return documentIds;
        }

        private async Task SaveFile(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(_documentsPath, fileName);
            // Klasörün varlığını kontrol et, yoksa oluştur
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        private string NormalizeFileName(string fileName)
        {

            fileName = fileName.ToUpper();
            // Türkçe karakterleri İngilizce karşılıkları ile değiştir
            fileName = fileName.Replace("ç", "c").Replace("ğ", "g")
                               .Replace("ı", "i").Replace("ö", "o")
                               .Replace("ş", "s").Replace("ü", "u")
                               .Replace("Ç", "C").Replace("Ğ", "G")
                               .Replace("İ", "I").Replace("Ö", "O")
                               .Replace("Ş", "S").Replace("Ü", "U");

            // Tüm harfleri büyük yap


            return fileName;
        }
        private string HashFileName(string fileName)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - byte dizisine dönüştürme, hash hesaplama
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(fileName));

                // Byte dizisini bir StringBuilder nesnesine dönüştür
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        public async Task AddMailLogDocument(int mailLogId, List<int> documentsId)
        {


            foreach (var documentId in documentsId)
            {
                var mailLogDocument = new MailLogDocuments
                {
                    MailLogId = mailLogId,
                    DocumentId = documentId
                };

                // Veritabanına ekleme işlemi
                await _uow.GetRepository<MailLogDocuments>().AddAsync(mailLogDocument);
            }

            // Değişiklikleri kaydet
            await _uow.SaveAsync();
        }

        public async Task DocumentDelete(int documentId)
        {


            var document = await _uow.GetRepository<Documents>().GetByIdAsync(documentId);


            string fullPath = Path.Combine(_documentsPath, document.DOKUMAN_ADI_GUID);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
               
            }
            await _uow.GetRepository<Documents>().DeleteAsync(document);

            await _uow.SaveAsync();

   
        }
    }

}
