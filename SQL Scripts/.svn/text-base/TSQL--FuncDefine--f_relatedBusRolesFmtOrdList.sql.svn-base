-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[f_relatedBusRolesFmtOrdList] (@val int)
RETURNS NVARCHAR(80)
AS
BEGIN
DECLARE @retval NVARCHAR(80)
DECLARE @fetchee NVARCHAR(80)
SET @retval = ''
DECLARE cursee CURSOR FOR
    select BR.c_u_Abbrev from t_RBSR_AUFW_u_BusRole BR
    LEFT OUTER JOIN  t_r_BusRoleWorkspaceEntitlement TT
    ON BR.c_id = TT.c_r_BusRole
    where TT.c_r_WorkspaceEntitlement  = @val
    ORDER BY BR.c_u_Abbrev
OPEN cursee;
FETCH NEXT FROM cursee INTO @fetchee;
WHILE @@FETCH_STATUS = 0
   BEGIN
      SET @retval = @retval + @fetchee + '<BR/>'
      FETCH NEXT FROM cursee INTO @fetchee;
   END;
CLOSE cursee;
DEALLOCATE cursee;
return @retval
END
GO

