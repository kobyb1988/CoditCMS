namespace Libs.ImageProcessing
{
	class NothingHandler : ImageHandler
	{
		protected override bool ProcessInternal(ImageDescriptor descriptor)
		{
			descriptor.DestinationRelativeName = descriptor.SourceRelativeName;
			return true;
		}
	}
}
