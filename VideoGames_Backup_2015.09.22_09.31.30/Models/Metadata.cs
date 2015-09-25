using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VideoGames.Models
{
    public class PlatformGameMetadata
    {
        [Display(Name ="Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ReleaseDate;

        [Display(Name ="Platform")]
        public int PlatformID;

        [Display(Name ="Game")]
        public int GameID;
    }

    public class RatingMetadata
    {
        [Display(Name ="Rating")]
        [DataType(DataType.Text)]
        public string RatingType;
    }

    public class PlatforomMetadata
    {
        [Display(Name ="Platform")]
        [DataType(DataType.Text)]
        public string PlatformName;

        [Display(Name ="Parent Company")]
        public int CompanyID;

        [Display(Name = "Type")]
        public int HardwareID;

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ReleaseDate;
    }

    public class ParentComapnyMetadata
    {
        [Display(Name ="Company")]
        [DataType(DataType.Text)]
        public string CompanyName;
    }

    public class HardwareMetadata
    {
        [Display(Name ="Type")]
        public string HardwareType;
    }

    public class GenreMetadata
    {
        [Display(Name ="Genre")]
        public string GenreName;
    }

    public class GameMetadata
    {
        [Display(Name ="Title")]
        [DataType(DataType.Text)]
        public string GameTitle;
    }

    public class DeveloperMetadata
    {
        [Display(Name ="Developer")]
        [DataType(DataType.Text)]
        public string DeveloperName;

        [Display(Name ="Founded")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateEstablished;
    }









}