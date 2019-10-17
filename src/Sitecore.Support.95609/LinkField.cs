namespace Sitecore.Support.Data.Fields
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Links;

    public class LinkField : Sitecore.Data.Fields.LinkField
    {
        public LinkField(Field innerField) : base(innerField)
        {
        }

        public LinkField(Field innerField, string runtimeValue) : base(innerField, runtimeValue)
        {
        }

        private string GetFriendlyUrl(Item item, bool shorten)
        {
            UrlOptions defaultOptions = UrlOptions.DefaultOptions;
            defaultOptions.ShortenUrls = shorten;
            return LinkManager.GetItemUrl(item, defaultOptions);
        }

        public override void ValidateLinks(LinksValidationResult result)
        {
            if (base.IsInternal)
            {
                if (!base.TargetID.IsNull || !string.IsNullOrEmpty(base.InternalPath))
                {
                    Item targetItem = base.TargetItem;
                    if (targetItem != null)
                    {
                        result.AddValidLink(targetItem, base.InternalPath);
                    }
                    else
                    {
                        #region Original code
                        result.AddBrokenLink(base.InternalPath);
                        #endregion
                    }
                }
            }
            else if (base.IsMediaLink && (!base.TargetID.IsNull || !string.IsNullOrEmpty(base.MediaPath)))
            {
                Item targetItem = base.TargetItem;
                if (targetItem != null)
                {
                    result.AddValidLink(targetItem, base.MediaPath);
                }
                else
                {
                    #region Original code
                    result.AddBrokenLink(base.MediaPath);
                    #endregion
                }
            }
        }
    }
}
