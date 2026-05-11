USE [NZBS]
GO

/****** Object:  Table [dbo].[nzbWFPPVInvoiceHistory]    Script Date: 09-May-26 11:35:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[nzbWFPPVInvoiceHistory](
	[ReceiptNumber] [varchar](17) NULL,
	[UserId] [varchar](100) NULL,
	[StatusDateTime] [datetime] NULL,
	[StatusId] [int] NULL,
	[Comment] [varchar](max) NULL,
	[IsManager] [int] NULL,
	[IsAdmin] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


