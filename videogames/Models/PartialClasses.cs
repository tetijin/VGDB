using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace VideoGames.Models
{
    [MetadataType(typeof(PlatformGameMetadata))]
    public partial class PlatformGame
    {
    }

    [MetadataType(typeof(RatingMetadata))]
    public partial class Rating
    {
    }

    [MetadataType(typeof(PlatforomMetadata))]
    public partial class Platform
    {
    }

    [MetadataType(typeof(ParentComapnyMetadata))]
    public partial class ParentCompany
    {
    }

    [MetadataType(typeof(HardwareMetadata))]
    public partial class Hardware
    {
    }

    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre
    {
    }

    [MetadataType(typeof(GameMetadata))]
    public partial class Game
    {
    }

    [MetadataType(typeof(DeveloperMetadata))]
    public partial class Developer
    {
    }

}