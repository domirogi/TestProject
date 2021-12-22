using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.MVC.AutoMapper
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            var list = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new StaticPagedList<TDestination>(list, source.GetMetaData());
        }
    }
}
