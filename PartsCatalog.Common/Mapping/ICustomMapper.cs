namespace PartsCatalog.Common.Mapping
{
    using AutoMapper;

    public interface ICustomMapper
    {
        void ConfigureMapping(Profile mapper);
    }
}
