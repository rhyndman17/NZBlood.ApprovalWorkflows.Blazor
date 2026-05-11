USE [NZBS]
GO

/****** Object:  Table [dbo].[nzbWFPPVInvoiceStatus]    Script Date: 09-May-26 11:35:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[nzbWFPPVInvoiceStatus](
	[ReceiptNumber] [varchar](17) NULL,
	[UserId] [varchar](100) NULL,
	[StatusDateTime] [datetime] NULL,
	[StatusId] [int] NULL,
	[ManagerApproval] [int] NULL
) ON [PRIMARY]
GO


