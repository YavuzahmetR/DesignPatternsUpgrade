namespace WebApp.Template.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
           return string.Empty;
        }

        protected override string SetPictureUrl()
        {
            return $@"<img src='/userpictures/defaultuserpicture.png' class='card-img-top'>";
        }
    }
}
