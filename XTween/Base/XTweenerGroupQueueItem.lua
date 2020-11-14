---@class XTweenerGroupQueueItem
XTweenerGroupQueueItem = class()

function XTweenerGroupQueueItem:Construction()
    ---@type table
    self.listTweeners = self.listTweeners or {}

    return self

end

function XTweenerGroupQueueItem:AddTweener(tweener)

    table.insert(self.listTweeners, tweener)
end

function XTweenerGroupQueueItem:Update(deltaTime)
    local list = self.listTweeners
    for i = 1, #list do
        local tweener = list[i]
        local state = tweener:GetState()
        if state == XTweenerState.BeforePlay then
            tweener:Play()
        end
        if state == XTweenerState.Playing then
            tweener:Update(deltaTime)
        end
    end
end

function XTweenerGroupQueueItem:CheckFinish()
    local finish = true
    local list  = self.listTweeners
    for i = 1, #list do
        if list[i]:GetState() ~= XTweenerState.End then
            finish = false
            break
        end
    end
    return finish
end