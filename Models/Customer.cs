using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
       
        [Required] //with this annotation above name our column name will not be null. but in VS2019 elle l'est plus déjà mais je laisse ça qd mm.
        [StringLength(255)]
        public string Name { get; set; }
       
        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType MembershipType { get; set; } //Ps: MembershipType (la 2éme) is called navigation property, because it allows us to navigate from one type to another (in this case from Customer to its MembershipType).
        
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } //EF reconize this convention and will treat this property as a foreign key.
       
        [Display(Name= "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; } //The ? means it's optional (nullable variable).
    }
}