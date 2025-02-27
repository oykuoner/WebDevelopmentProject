namespace LSDCS.Web.ResultMessages
{
    public static class Message
    {

        public static class MailLog
        {

            public static string Add(string mailLogTitle)
            {

                return $"{mailLogTitle} konulu mail eklenmiştir.";
            }

            public static string Update(string mailLogTitle)
            {

                return $"{mailLogTitle} konulu mail güncellenmiştir.";
            }

            public static string Delete(string mailLogTitle)
            {

                return $"{mailLogTitle} konulu mail silinmiştir.";
            }
            public static string UndoDelete(string mailLogTitle)
            {

                return $"{mailLogTitle} konulu mail geri alınmıştır.";
            }

        }

        public static class User
        {

            public static string Add(string userName)
            {

                return $"{userName}  mail adresli kullanıcı başarıyla eklenmiştir.";
            }

            public static string Update(string userName)
            {

                return $"{userName}  mail adresli kullanıcı başarıyla güncellenmiştir.";
            }

            public static string Delete(string userName)
            {

                return $"{userName} mail adresli kullanıcı başarıyla silinmiştir.";
            }


        }

    }
}
