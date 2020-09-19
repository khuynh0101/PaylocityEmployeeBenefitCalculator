namespace Paylocity.Common.Cache
{
    public interface ICacheManager<KKey, TObject>
    {
        void Save(KKey key, TObject objectToSave);

        TObject Retrieve(KKey key);

        void Remove(KKey key);
    }
}
