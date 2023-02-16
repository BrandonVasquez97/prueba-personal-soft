
namespace SoftApi.Models
{
    public class Policy
    {
        public int id { get; set; }
        public string name_client { get; set; }
        public string dni_client { get; set; }
        public DateTime date_of_birth { get; set; }
        public DateTime date_of_start_policy { get; set; }
        public string coberture_policy { get; set; }
        public int max_value { get; set; }
        public string name_of_policy { get; set; }
        public string city_of_client { get; set; }
        public string address_of_client { get; set; }
        public string plate { get; set; }
        public string model_of_car { get; set; }
        public string inspection_vehicle { get; set; }
        public DateTime date_end_of_policy { get; set; }
    }
}