//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Esoft_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class AgentsSet
    {
        public AgentsSet()
        {
            this.DemandSet = new HashSet<DemandSet>();
            this.SupplySet = new HashSet<SupplySet>();
        }
    
        public int id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> DealShare { get; set; }
    
        public virtual ICollection<DemandSet> DemandSet { get; set; }
        public virtual ICollection<SupplySet> SupplySet { get; set; }
    }
}
