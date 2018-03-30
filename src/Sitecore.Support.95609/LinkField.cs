// Sitecore.Support.Data.Fields.LinkField
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Support.Data.Fields
{
    public class LinkField : Sitecore.Data.Fields.LinkField
    {
        public LinkField(Field innerField)
        : base(innerField)
        {
        }

        public LinkField(Field innerField, string runtimeValue)
        : base(innerField, runtimeValue)
        {
        }


        public override void ValidateLinks(LinksValidationResult result)
        {
            if (this.IsInternal)
            {
                if (this.TargetID.IsNull && string.IsNullOrEmpty(this.InternalPath))
                {
                    return;
                }
                Item targetItem = this.TargetItem;
                if (targetItem != null)
                {
                    result.AddValidLink(targetItem, this.InternalPath);
                }
                else
                {
                    result.AddBrokenLink(((object)this.TargetID).ToString());
                }
            }
            else if (this.IsMediaLink)
            {
                if (this.TargetID.IsNull && string.IsNullOrEmpty(this.MediaPath))
                {
                    return;
                }
                Item targetItem2 = this.TargetItem;
                if (targetItem2 != null)
                {
                    result.AddValidLink(targetItem2, this.MediaPath);
                }
                else
                {
                    result.AddBrokenLink(((object)this.TargetID).ToString());
                }
            }
        }

        private string GetFriendlyUrl(Item item, bool shorten)
        {
            UrlOptions defaultOptions = UrlOptions.DefaultOptions;
            defaultOptions.ShortenUrls = shorten;
            return LinkManager.GetItemUrl(item, defaultOptions);
        }
    }
}