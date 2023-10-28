// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.FileProviders;

namespace Include
{
    [HtmlTargetElement("include-content", Attributes = "name", TagStructure = TagStructure.WithoutEndTag)]
    public class IncludeHtmlTagHelper : TagHelper
    {
        private readonly IFileProvider fileProvider;

        public IncludeHtmlTagHelper(IWebHostEnvironment hostEnvironment)
        {
            fileProvider = hostEnvironment.ContentRootFileProvider;
        }

        /// <summary>
        /// The name or path of the partial view that is rendered to the response.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the <see cref="Rendering.ViewContext"/> of the executing view.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <inheritdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Reset the TagName. We don't want `partial` to render.
            output.TagName = null;

            var filePath = Path.Combine(Path.GetDirectoryName(ViewContext.ExecutingFilePath), Name);

            var file = fileProvider.GetFileInfo(filePath);
            if (!file.Exists)
            {
                throw new FileNotFoundException($"Unable to find file {Name} associated with {ViewContext.ExecutingFilePath}", Name);
            }

            await using var fileStream = file.CreateReadStream();
            using var streamReader = new StreamReader(fileStream);

            var lines = new HtmlContentBuilder();
            string line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                lines.AppendHtmlLine(line);
            }

            output.Content.SetHtmlContent(lines);
        }
    }
}