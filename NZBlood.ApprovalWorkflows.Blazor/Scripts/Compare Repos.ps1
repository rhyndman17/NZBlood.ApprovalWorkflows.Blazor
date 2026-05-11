git fetch NZBlood.ApprovalWorkflows.Blazor
git status --short --branch
git diff --stat main..NZBlood.ApprovalWorkflows.Blazor/main
git log --left-right --cherry-pick --oneline main...NZBlood.ApprovalWorkflows.Blazor/main
