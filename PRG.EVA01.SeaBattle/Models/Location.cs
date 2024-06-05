namespace PRG.EVA01.Anthony.Mohamed.Models
{
    public class Location
    {
        public int Id { get; set; }  
        private string _letter;
        private string _number;

        public string Letter
        {
            get => _letter;
            set
            {
                // Check if the letter is between A and F
                if (value.Length == 1 && value[0] >= 'A' && value[0] <= 'F')
                {
                    _letter = value;
                }
                else
                {
                    throw new ArgumentException("Letter must be between A and F");
                }
            }
        }

        public string Number
        {
            get => _number;
            set
            {
                // Check if the number is between 1 and 6
                if (value.Length == 1 && value[0] >= '1' && value[0] <= '6')
                {
                    _number = value;
                }
                else
                {
                    throw new ArgumentException("Number must be between 1 and 6");
                }
            }
        }

        public static implicit operator Location(string v)
        {
            throw new NotImplementedException();
        }
    }
}
