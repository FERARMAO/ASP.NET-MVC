using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel //dans cette class, on hérite que des propriété de Customer et MembershipType classes, mais pas de leur contenu !!! TRES IMPORTANT. Le contenu c'est dans le Controller qu'on le déclare avec ActionResult(que ce soit de la BD ou par hard coding).
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

    }
}