﻿using System.ComponentModel.DataAnnotations;

namespace VideoEducation.Microservices.Catalog.Api.Options {
    public class MongoOption
    {
        [Required] public string DatabaseName { get; set; } = default!;
        [Required] public string ConnectionString { get; set; } = default!;

    }
}