using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hbx.Generic;
using Hbx.Assets;

public class Load : GenericSingleton<Load>
{
    List<ILoader> _loaders = new List<ILoader>();

    public Load()
    {
        AddLoader(new DiskByteLoader());
        AddLoader(new DiskTextLoader());
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

    public ILoader GetLoaderForPath(string path)
    {
        // first find loaders to handle the protocol, this is because
        // things like json, resource, addressable don't use extensions
        // meaning the protocol is the key piece of information
        Protocol protocol = Protocols.ProtocolForPath(path);
        List<ILoader> ploaders = GetLoadersForProtocol(protocol);

        if(ploaders.Count == 0)
        {
            // no loader for protocol
            return null;
        }

        bool requiresread = Protocols.ProtocolRequiresRead(protocol);
        if (requiresread)
        {
            // find loaders for the extension
            string ext = Path.GetExtension(path);
            List<ILoader> extloaders = GetLoadersForExtension(ext);

            if(extloaders.Count == 0)
            {
                // no loader for extension
                return null;
            }

            // what type of input it needs
            LoaderInputType inputmask = extloaders[0].inputTypeMask;

            //find the first protocol loader that can provide the input we need
            foreach(ILoader ploader in ploaders)
            {
                if(ploader.)
            }

        }

        // now determine if one of the loaders can directly load the path
        foreach(ILoader ploader in ploaders)
        {
            if(ploader.inputTypeMask.HasFlag(LoaderInputType.Path))
            {

            }
        }
        return null;
    }

    public List<ILoader> GetLoadersForExtension(string ext)
    {
        List<ILoader> loaders = new List<ILoader>();
        foreach (ILoader loader in _loaders)
        {
            if (loader.supportsExtension(ext))
            {
                loaders.Add(loader);
            }
        }
        return loaders;
    }

    public List<ILoader> GetLoadersForProtocol(Protocol protocol)
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
        return loaders;
    }
}
