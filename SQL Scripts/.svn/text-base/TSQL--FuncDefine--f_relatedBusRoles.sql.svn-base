set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER function [dbo].[f_relatedBusRoles] (@val int)
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
      SET @retval = @retval + @fetchee + ' '
      FETCH NEXT FROM cursee INTO @fetchee;
   END;
CLOSE cursee;
DEALLOCATE cursee;
return @retval
END

