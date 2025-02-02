using System.Text;
using WebApp.Template.Models;

namespace WebApp.Template.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            var sb = new StringBuilder();
            sb.Append("<a href='#' class='card-link'>Mesaj gönder</a>");
            sb.Append("<a href='#' class='card-link'>Detaylı profil</a>");
            return sb.ToString();
        }

        protected override string SetPictureUrl()
        {
            return $@"<img src='{User.PictureUrl}' class='card-img-top'>";
        }
    }
}
