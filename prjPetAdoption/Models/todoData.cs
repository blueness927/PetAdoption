//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjPetAdoption.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class todoData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public todoData()
        {
            this.petTodo = new HashSet<petTodo>();
        }
    
        public int todoDataID { get; set; }
        public string todoDataTitle { get; set; }
        public string todoDataContent { get; set; }
        public string todoDataDocument { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<petTodo> petTodo { get; set; }
    }
}
