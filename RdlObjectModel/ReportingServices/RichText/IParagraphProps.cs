﻿using Microsoft.ReportingServices.RPLObjectModel;

namespace Microsoft.ReportingServices.RichText
{
	internal interface IParagraphProps
	{
		float SpaceBefore { get; }

		float SpaceAfter { get; }

		float LeftIndent { get; }

		float RightIndent { get; }

		float HangingIndent { get; }

		int ListLevel { get; }

		RPLFormat.ListStyles ListStyle { get; }

		RPLFormat.TextAlignments Alignment { get; }

		int ParagraphNumber { get; set; }
	}
}
