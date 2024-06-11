using System.ComponentModel;
namespace Course_Work4
{
    public class User
    {
        [DisplayName("ID")]       
        public int ID { get; internal set; }
        [DisplayName("Название")]
        public string? Name { get; internal set; }
        [DisplayName("Масса(тонн)")]
        public string? Weight { get; internal set; }
        [DisplayName("Скорость(км/ч)")]
        public string? Speed { get; internal set; }
        [DisplayName("Материал")]
        public string? Material { get; internal set; }
        [DisplayName("Срок службы(лет)")]
        public string? ServiceLife { get; internal set; }
    }
}