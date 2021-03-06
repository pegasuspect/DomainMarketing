﻿using System;
using Cinar.Database;
using DealerSafe2.API.Entity.LifeCycles;

namespace DealerSafe2.API.Entity.Products
{
    public class Supplier : NamedEntity, ICriticalEntity
    {
        [ColumnDetail(Length = 12)]
        public string ApiId { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [ColumnDetail(Length = 12)]
        public string LifeCycleId { get; set; }

        [ColumnDetail(Length = 12)]
        public string PropertySetId { get; set; }

        public ApiRelated.Api Api() { return Provider.ReadEntityWithRequestCache<ApiRelated.Api>(ApiId); }
        public LifeCycle LifeCycle() { return Provider.ReadEntityWithRequestCache<LifeCycle>(LifeCycleId); }
    }

    public class ViewSupplier : Supplier
    {
        public string ApiName { get; set; }
    }
}