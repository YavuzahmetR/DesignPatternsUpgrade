using System.Text;
using WebApp.Template.Models;

namespace WebApp.Template.UserCards
{
    public abstract class UserCardTemplate
    {
        protected AppUser User { get; set; }
        public void SetUser(AppUser user)
        {
            User = user;
        }

        public string Build()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class='card'>");
            sb.AppendLine(SetPictureUrl());
            sb.AppendLine($@"<div class='card-body'>
                             <h5>{User.UserName}</h5>
                             <p>{User.Description}</p>");
            sb.AppendLine(SetFooter());
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return sb.ToString();

        }



        protected abstract string SetPictureUrl();
        protected abstract string SetFooter();
    }
}
