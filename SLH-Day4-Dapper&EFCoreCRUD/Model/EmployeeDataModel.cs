using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoLearning.SLH_Day4_Dapper_EFCoreCRUD.Model
{
    public class EmployeeDataModel
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public bool IsGroup { get; set; }
        public string TextCss { get; set; }
        public string BackgroundCss { get; set; }

        public string AspNetUserId { get; set; }

        public string ImageFileName => $"images/Employee/{Name.Replace(" ", "")}.jpg";

        public long? VacationEntitlement { get; set; }

        public long? VacationSaldo { get; set; }

        public long? AuthorizedAbsenceSaldo { get; set; }

        public long? AuthorizedLeaveSaldo { get; set; }

        /// <summary>
        /// This is the property to retrieve the data from database
        /// Since the name matches the column name of database
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// This is the property to bind SignaturePad
        /// </summary>
        public byte[] SignatureAsByte { get; set; }

        /// <summary>
        /// This is the data to save in database and to use as the Binding value of image(img) component
        /// </summary>
        public string SignatureAsBase64 => Encoding.UTF8.GetString(this.SignatureAsByte);

        public override bool Equals(object obj)
        {
            EmployeeDataModel resource = obj as EmployeeDataModel;
            return resource != null && resource.Id == Id;
        }
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Use this method after "Signature" value is surely assigned from DB
        /// </summary>
        public void AssignSignatureAsByte()
        {
            this.SignatureAsByte = Encoding.UTF8.GetBytes(this.Signature ?? string.Empty);
        }

        [NotMapped]
        public byte[]? ImageContent { get; set; }
        public string? ImageName { get; set; }
    }
}
