//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoGames.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            this.PlatformGames = new HashSet<PlatformGame>();
        }
    
        public int GameID { get; set; }
        public int DeveloperID { get; set; }
        public int GenreID { get; set; }
        public int RatingID { get; set; }
        public Nullable<decimal> MSRP { get; set; }
        public string GameTitle { get; set; }
    
        public virtual Developer Developer { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Rating Rating { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlatformGame> PlatformGames { get; set; }
    }
}