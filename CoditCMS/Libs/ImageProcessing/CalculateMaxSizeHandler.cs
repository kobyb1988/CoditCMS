﻿using System.Text;

namespace Libs.ImageProcessing
{
	class CalculateMaxSizeHandler : CalculateSizeHandler
	{
		protected override bool ShouldBeRecalculated(double resultSize, double targetSize)
		{
			return resultSize < targetSize;
		}

		protected override void PostProcess(ImageDescriptor descriptor)
		{
			base.PostProcess(descriptor);

			var postfix = (StringBuilder)descriptor.Parameters[ImageDescriptor.ParametersNames.Postfixes] ?? new StringBuilder();
			postfix.Append("_f");
			descriptor.Parameters[ImageDescriptor.ParametersNames.Postfixes] = postfix;
		}
	}
}
