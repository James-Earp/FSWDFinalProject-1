using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSWDFinalProject.DATA.EF
{
    public class ApplicationMetadata
    {
        #region Application
        //public int ApplicationId { get; set; }

        [Required(ErrorMessage = "* Id of Open Position is Required.")]
        public int OpenPositionId { get; set; }

        [Required(ErrorMessage = "* Id of User is Required.")]
        public string UserId { get; set; }

        [Display(Name = "Date of Application")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime ApplicationDate { get; set; }

        [Display(Name = "Manager Notes")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string ManagerNotes { get; set; }

        [Required(ErrorMessage = "* Application Staus is Required.")]
        public int ApplicationStatusId { get; set; }

        [Required(ErrorMessage = "* Resume File is Required.")]
        public string ResumeFilename { get; set; }
        #endregion
    }
    public class ApplicationStatusMetadata
    { 
        #region Application Status
        //public int ApplicationStatusId { get; set; }

        [Required(ErrorMessage = "* Status Name is Required.")]
        [StringLength(50, ErrorMessage = "* Status Name must be 50 characters or less.")]
        public string StatusName { get; set; }

        [Display(Name = "Status Description")]
        [StringLength(250, ErrorMessage = "* Status Description must be 250 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [UIHint("MultilineText")]
        public string StatusDescription { get; set; }
        #endregion
    }

    public class LocationMetadata
    {
        #region Location
        //public int LocationId { get; set; }

        [Required(ErrorMessage = "* Store Number is Required.")]
        public string StoreNumber { get; set; }

        [Required(ErrorMessage = "* City is Required.")]
        [StringLength(50, ErrorMessage = "* City must be 50 characters or less.")]
        public string City { get; set; }

        [Required(ErrorMessage = "* State is Required.")]
        [StringLength(2, ErrorMessage = "* State must be 2 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "* ManagerId is Required.")]
        [StringLength(128, ErrorMessage = "* City must be 128 characters or less.")]
        public string ManagerId { get; set; }
        #endregion
    }

    public class OpenPositionMetadata
    {
        #region Open Positions
        //public int OpenPositionId { get; set; }

        [Required(ErrorMessage = "* Id of Position is Required.")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "* Location is Required.")]
        public int LocationId { get; set; }
        #endregion
    }

    public class PositionMetadata
    {
        #region Position
        //public int PositionId { get; set; }

        [Required(ErrorMessage = "* Title of Position is Required.")]
        [StringLength(50, ErrorMessage = "* Title must be 50 characters or less.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "* Job Description is Required.")]
        [UIHint("MultilineText")]
        public string JobDescription { get; set; }
        #endregion
    }

    public class UserDetailMetadata
    {
        #region User Details
        //public string UserId { get; set; }

        [Required(ErrorMessage = "* First Name is Required.")]
        [StringLength(50, ErrorMessage = "* First Name must be 50 characters or less.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Last Name is Required.")]
        [StringLength(50, ErrorMessage = "* Last Name must be 50 characters or less.")]
        public string LastName { get; set; }

        [Display(Name = "Resume File")]
        public string ResumeFilename { get; set; }
    }
        [MetadataType(typeof(UserDetailMetadata))]
        public partial class UserDetail
        {
            [Display(Name = "Full Name")]
            public string FullName
            {
                get { return FirstName + " " + LastName; }
            }
        }
        #endregion
}
