namespace Lab08.MVC.Business
{
    public class OperationDetails
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

        public OperationDetails(bool succeed, string message, string property = "")
        {
            Succeed = succeed;
            Message = message;
            Property = property;
        }
    }
}
