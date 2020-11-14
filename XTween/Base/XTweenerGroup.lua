---@class XTweenerGroup
XTweenerGroup = class()

function XTweenerGroup:Construction()
    self.data = nil
    ---@type XTweenerState
    self.state = XTweenerState.BeforePlay
    self.delay = 0
    ---@type Action<XTweenerIF>
    self.finishAction = nil
    ---@type Action<XTweenerIF>
    self.startAction = nil
    self.speed = 0
    ---@type XTweenerGroupQueueItem[]
    self.listQueueItems = {}
    self.controlByParent = false
    self.shouldCallback = true

    return self

end

function XTweenerGroup:SetTweenerData(data)
    self.data = data
end

function XTweener:GetTweenerData()
    return self.data
end

function XTweenerGroup:SetDelay(delay)
    self.delay = delay
end

function XTweenerGroup:GetDelay()
    return self.delay
end

function XTweenerGroup:Play()
    if self.state == XTweenerState.Playing then
        return
    end

    if self.state == XTweenerState.BeforePlay then
        if self.startAction then
            self.startAction(self)
        end
    end
    self.state = XTweenerState.Playing
    if not self.controlByParent then
        XTweenUpdater.AddTween(self)
    end
end

function XTweenerGroup:Finish(shouldCallback)
    self.state = XTweenerState.End
    self.shouldCallback = shouldCallback
end

function XTweenerGroup:Pause()
    if self.state == XTweenerState.Playing then
        self.state = XTweenerState.Pause
    end
end

function XTweenerGroup:Resume()
    if self.state == XTweenerState.Pause then
        self.state = XTweenerState.Playing
    end
end

function XTweenerGroup:SetFinishCallback(action)
    self.finishAction = action
end

---@param action Action<XTweenerIF>
function XTweenerGroup:SetStartCallback(action)
    self.startAction = action
end

function XTweenerGroup:GetState()
    return self.state
end

function XTweenerGroup:SetSpeed(speed)
    self.speed = speed
end

function XTweenerGroup:Update(deltaTime)

    -----此个判断因为observer 状态引起
    if self.state == XTweenerState.End then
        return
    end

    if #(self.listQueueItems) > 0 then
        self.listQueueItems[1]:Update(deltaTime)
        if self.listQueueItems[1]:CheckFinish() then
            table.remove(self.listQueueItems, 1)
        end
    end

    if #(self.listQueueItems) == 0 then
        self.state = XTweenerState.End
    end

    if self.state == XTweenerState.End then
        ---正常流程应该remove掉observer
        if self.observer ~= nil then
            self.observer:RemoveTweener(self)
        end

        if self.finishAction and self.shouldCallback then
            self.finishAction(self)
        end
    end

end

function XTweenerGroup:SetControlByParent()
    self.controlByParent = true
end

---@param addMode XTweenGroupAddMode
function XTweenerGroup:AddTweener(tweener, addMode)
    tweener:SetControlByParent()
    local queueItem = nil
    local list = self.listQueueItems
    if addMode == XTweenGroupAddMode.Queue or #list == 0 then
        queueItem = XTweenerGroupQueueItem.new():Construction()
        table.insert(list, queueItem)
    else
        queueItem = list[#(self.listQueueItems)]
    end

    queueItem:AddTweener(tweener)
end

-----添加observer后，会跟踪observer target 的状态
function XTweenerGroup:AddObserverObj(target)
    ---@type XTweenObserver
    self.observer = LuaBehaviour.Get(target.gameObject, XTweenObserver)
    if not self.observer then
        self.observer = LuaBehaviour.Add(target.gameObject, XTweenObserver)
    end
    self.observer:AddXTweener(self)
end

function XTweenerGroup:GetControlByParent()
    return self.controlByParent
end

function XTweenerGroup:GetTypeMark()
    return 'XTweenerGroup'
end

------------observer 回调

function XTweenerGroup:BeReplaced()
    ---do nothing
end

function XTweenerGroup:OnEnable()
    ---do nothing
end

function XTweenerGroup:OnDisable()
    ---终止整个tween
    self.state = XTweenerState.End
    self.observer:RemoveTweener(self)
end

function XTweenerGroup:OnDestroy()
    ---终止整个tween
    self.state = XTweenerState.End
    self.observer:RemoveTweener(self)
end

---observer 回调----------------









































