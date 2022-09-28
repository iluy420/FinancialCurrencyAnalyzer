using Attributes;

namespace DataBase.Core.Enums
{
    public enum EmailDomainEnum
    {
        [StringValue("@gmail.com")]
        GMAIL = 1,

        [StringValue("@yandex.ru")]
        YANDEX = 2,

        [StringValue("@mail.ru")]
        MAIL = 3,

        [StringValue("@yahoo.com")]
        YAHOO = 4
    }
}
