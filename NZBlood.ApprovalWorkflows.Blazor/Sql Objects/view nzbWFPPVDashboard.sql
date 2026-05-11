USE [NZBS]
GO

/****** Object:  View [dbo].[nzbWFPPVDashboard]    Script Date: 09-May-26 11:33:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE view [dbo].[nzbWFPPVDashboard] as
with approvalsPending as ( 
select r.ReceiptNumber,r.VendorID,r.VendorName,r.VendorDocNumber,r.UserID,r.CreatedDateTime,s.ManagerApproval,r.Comment,r.UserEmailAddress
from nzbWFPPVTransactionHdr  r
join nzbWFPPVInvoiceStatus s on s.ReceiptNumber=r.ReceiptNumber
where s.StatusId=1
),
approvers as (
--select ReceiptNumber,STRING_AGG(AccountName, ' * ') AS Approvers
--			from nzbWFPPVApproverList
--			group by ReceiptNumber
			select t1.ReceiptNumber, stuff((select ' * ' + AccountName
					from nzbWFPPVApproverList t2
					where t1.ReceiptNumber=t2.ReceiptNumber
					for XML PATH('')),1,2,'')  as Approvers
			from nzbWFPPVApproverList t1
			--join POP10300 p on p.POPRCTNM=t1.ReceiptNumber
			group by ReceiptNumber
),
purchaseOrders as (
select ReceiptNumber,PONumber 'PONumber' from nzbWFPPVTransactionLne group by ReceiptNumber,PONumber),
firstLevelApprover as (
	select h.ReceiptNumber,h.UserID 
	from nzbWFPPVInvoiceHistory h 
		join nzbWFPPVInvoiceStatus s on h.ReceiptNumber= s.ReceiptNumber
	where h.StatusId=2 and IsManager=0 and s.ManagerApproval=1 )

select h.ReceiptNumber,h.VendorID,rtrim(h.VendorName) 'VendorName',h.VendorDocNumber,h.UserID 'SubmittedBy',h.UserEmailAddress 'SubmittedByEmailAddress', h.CreatedDateTime 'SubmittedDateTime',a.Approvers,p.PurchaseOrders,h.ManagerApproval,
	case h.ManagerApproval when 1 then 'Yes' else 'No' end as ManagerApprovalDisplay,h.Comment,isnull(max(f.UserId),'') 'FirstLevelApprover'
from approvalsPending h
join approvers a on a.ReceiptNumber=h.ReceiptNumber
join (select t1.ReceiptNumber, stuff((select ' * ' + POnumber
		from nzbWFPPVTransactionLne t2
		where t1.ReceiptNumber=t2.ReceiptNumber
		group by ReceiptNumber,PONumber
		for XML PATH('')),1,2,'' )  as PurchaseOrders 
		from nzbWFPPVTransactionLne t1) p on p.ReceiptNumber=h.ReceiptNumber
left join firstLevelApprover f on f.ReceiptNumber=h.ReceiptNumber
group by  h.ReceiptNumber,h.VendorID,h.VendorName,h.VendorDocNumber,h.UserID,h.UserEmailAddress,h.CreatedDateTime,a.Approvers,p.PurchaseOrders,h.ManagerApproval,h.Comment
			
GO


