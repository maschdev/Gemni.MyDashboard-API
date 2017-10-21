namespace MyDashboard.Domain.ValueObjects
{
    public class Document
    {
        protected Document() { }

        public Document(string number)
        {
            Number = string.IsNullOrWhiteSpace(number) ? null : number.Trim();
        }

        public string Number { get; private set; }

        public void Update(string number) => Number = number;
    }
}
