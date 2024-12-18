﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Domains;

public class Like
{
    public long Id { get; set; }
    public DateTime DateTime { get; set; }
    public long LikedByUser { get; set; }
    [ForeignKey(nameof(LikedByUser))] public User User { get; set; }

}