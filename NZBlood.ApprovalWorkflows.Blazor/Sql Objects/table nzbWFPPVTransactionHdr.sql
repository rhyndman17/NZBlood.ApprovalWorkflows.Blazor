USE [NZBS]
GO

/****** Object:  Table [dbo].[nzbWFPPVTransactionHdr]    Script Date: 09-May-26 11:34:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[nzbWFPPVTransactionHdr](
	[ReceiptNumber] [char](17) NOT NULL,
	[VendorDocNumber] [char](21) NULL,
	[VendorID] [char](15) NULL,
	[VendorName] [char](65) NULL,
	[UserID] [char](15) NULL,
	[UserEmailAddress] [varchar](100) NULL,
	[CreatedDateTime] [datetime] NULL,
	[DocStatus] [int] NULL,
	[Comment] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


