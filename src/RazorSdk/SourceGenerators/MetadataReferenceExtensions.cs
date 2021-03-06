// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;

namespace Microsoft.NET.Sdk.Razor.SourceGenerators
{
    internal static class MetadataReferenceExtensions
    {
        public static Guid? GetModuleVersionId(this MetadataReference reference, Compilation compilation)
        {
            var symbol = compilation.GetAssemblyOrModuleSymbol(reference);

            if (symbol is IAssemblySymbol assemblySymbol)
            {
                foreach (var module in assemblySymbol.Modules)
                {
                    try
                    {
                        return module.GetMetadata().GetModuleVersionId();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return null;
            }

            if (symbol is IModuleSymbol moduleSymbol)
            {
                var metadata = moduleSymbol.GetMetadata();
                try
                {
                    return metadata.GetModuleVersionId();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
