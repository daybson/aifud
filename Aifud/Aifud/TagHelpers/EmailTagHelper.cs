using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aifud.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EnderecoEmail { get; set; }
        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + EnderecoEmail);
            output.Content.SetContent(Conteudo);

            base.Process(context, output);
        }
    }
}
