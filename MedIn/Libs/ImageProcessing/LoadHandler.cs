namespace Libs.ImageProcessing
{
	class LoadHandler : ImageHandler
	{
		protected override bool ProcessInternal(ImageDescriptor descriptor)
		{
			var fullpath = HttpContext.Current.Server.MapPath(descriptor.SourceRelativeName);
			var image = Image.FromFile(fullpath);

			descriptor.Parameters.Add(ImageDescriptor.ParametersNames.FullLocalPath, fullpath);
			descriptor.Parameters.Add(ImageDescriptor.ParametersNames.SourceImage, image);

			return true;
		}
	}
}
