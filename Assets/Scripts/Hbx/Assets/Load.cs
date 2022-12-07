using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Hbx.Generic;
using Hbx.Assets;

public class Load : GenericSingleton<Load>
{
    List<ILoader> _loaders = new List<ILoader>();

    public Load()
    {
        AddLoader(new DiskLoader());
        AddLoader(new HttpLoader());
        AddLoader(new JsonLoader());
        AddLoader(new ResourcesLoader());
        AddLoader(new TextureLoader());
    }

    public void AddLoader(ILoader loader)
    {
        _loaders.Add(loader);
    }

    public void RemoveLoader(ILoader loader)
    {
        if(_loaders.Contains(loader))
        {
            _loaders.Remove(loader);
        }
    }

    public async Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options = null)
    {
        if (options == null) options = new LoaderOptions();
        options.setOriginalSrcIfEmpty(src);

        // Find loaders to handle the protocol, this is because
        // things like json://, resource://, address:// don't use extensions
        // meaning the protocol is the key piece of information
        Protocol protocol = Protocols.ProtocolForPath(src);
        ILoader ploader = GetLoaderForProtocol(protocol);

        if (ploader == null)
        {
            // no loader for protocol
            return new ILoaderResult<T>(); ;
        }

        // check if we first need to fetch the raw data, i.e. it's a file that needs
        // fetching from disk or http server then loading with an addtional reader
        if (!IsRawType(typeof(T)) && Protocols.ProtocolRequiresFetch(protocol))
        {
            // find loader for the extension, check the protocol loader first as http can handle textures etc
            string ext = Path.GetExtension(src);
            ILoader extloader = ploader.supportsExtension(ext) ? ploader : GetLoaderForExtension(ext);

            if (extloader == null)
            {
                // no loader for extension, if there is no extension see
                // if we can find a loader for the type
                if(string.IsNullOrEmpty(ext))
                {
                    extloader = ploader.supportsType(typeof(T)) ? ploader : GetLoaderForType(typeof(T));
                }

                if (extloader == null)
                {
                    return new ILoaderResult<T>(); ;
                }
            }

            // if our ploader is different from our extloader we will first
            // read the data as bytes using the ploader then load the asset
            // type T using the extloader
            if (ploader != extloader)
            {

                // read the data as bytes
                ILoaderResult<byte[]> bytes = await ploader.ReadAsync<byte[]>(src, null);

                if (!bytes.valid)
                {
                    // invalid data read from src
                    return new ILoaderResult<T>();
                }

                // the raw data loaded successfully, now load the asset
                // with the extension loader and raw bytes data
                return await extloader.ReadAsync<T>(bytes, options); // TODO this could fail loading a Texture2D with no extension via the http loader i.e. it could be a dds file
            }
        }

        // load directly with the protocol loader and src
        return await ploader.ReadAsync<T>(src, options); // TODO should we validate the supported type before trying to load?
    }

    public ILoader GetLoaderForType(Type type)
    {
        List<ILoader> loaders = new List<ILoader>();
        foreach (ILoader loader in _loaders)
        {
            if (loader.supportsType(type))
            {
                loaders.Add(loader);
            }
        }
        return loaders.Count > 0 ? loaders[0] : null;
    }

    public ILoader GetLoaderForExtension(string ext)
    {
        List<ILoader> loaders = new List<ILoader>();
        foreach (ILoader loader in _loaders)
        {
            if (loader.supportsExtension(ext))
            {
                loaders.Add(loader);
            }
        }
        return loaders.Count > 0 ? loaders[0] : null;
    }

    public ILoader GetLoaderForProtocol(Protocol protocol)
    {
        string protocolprefix = Protocols.PrefixForProtocol(protocol);
        List<ILoader> loaders = new List<ILoader>();
        foreach(ILoader loader in _loaders)
        {
            if(loader.supportsProtocol(protocolprefix))
            {
                loaders.Add(loader);
            }
        }
        return loaders.Count > 0 ? loaders[0] : null;
    }

    public static bool IsRawType(Type type)
    {
        return type == typeof(byte[]) || type == typeof(string);
    }
}
