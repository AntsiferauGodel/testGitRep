namespace Lab08.MVC.Web.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ElementsOnPage { get; set; }
        public string ActionNavigationName { get; set; }
        public string UserId { get; set; }
        public string SearchWord { get; set; }
    }
}