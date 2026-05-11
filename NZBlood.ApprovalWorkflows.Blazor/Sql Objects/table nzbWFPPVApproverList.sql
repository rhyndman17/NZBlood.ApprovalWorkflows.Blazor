USE [NZBS]
GO

/****** Object:  Table [dbo].[nzbWFPPVApproverList]    Script Date: 09-May-26 11:34:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[nzbWFPPVApproverList](
	[ReceiptNumber] [char](17) NULL,
	[ApproverName] [varchar](100) NULL,
	[EmailAddress] [varchar](100) NULL,
	[AccountName] [varchar](100) NULL
) ON [PRIMARY]
GO


