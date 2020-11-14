---@class XTweenerAction
XTweenerAction = class()

---@return XTweenerAction
function XTweenerAction:Construction()
    ---@type userdata
    self.data = nil
    self.delay = 0
    ---@type XTweenerState
    self.state = XTweenerState.BeforePlay
    self.finishAction = nil
    self.startAction = nil
    self.actionProcess = nil
    self.currentTime = 0
    self.timeProgress = 0
    self.speed = 1.0
    self.currentRepeatTimes = 0
    self.controlByParent = false
    return self
end

function XTweenerAction:GetState()
    return self.state
end

function XTweenerAction:SetTweenerData(data)
    self.data = data
end

function XTweenerAction:GetTweenerData()
    return self.data
end

function XTweenerAction:SetDelay(delay)
    self.delay = delay
end

function XTweenerAction:GetDelay()
    return self.delay
end

function XTweenerAction:Play()
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

function XTweenerAction:Finish()
    self.state = XTweenerState.End
end

function XTweenerAction:Pause()
    if self.state == XTweenerState.Playing then
        self.state = XTweenerState.Pause
    end
end

function XTweenerAction:Resume()
    if self.state == XTweenerState.Pause then
        self.state = XTweenerState.Playing
    end
end

---@param action Action<XTweenerIF>
function XTweenerAction:SetFinishCallback(action)
    self.finishAction = action
end

---@param action Action<XTweenerIF>
function XTweenerAction:SetStartCallback(action)
    self.startAction = action
end
---@param speed float
function XTweenerAction:SetSpeed(speed)
    self.speed = speed
end

function XTweenerAction:SetControlByParent()
    self.controlByParent = true
end

---@param deltaTime float
function XTweenerAction:Update(deltaTime)
    self.currentTime = self.currentTime + deltaTime * self.speed
    if self.currentTime >= self.delay then
        if self.actionProcess then
            self.actionProcess(self)
        end
        if self.finishAction then
            self.finishAction(self)
        end
        self.state = XTweenerState.End
    end
end
---@param action Action<XTweenerIF>
function XTweenerAction:SetAction(action)
    self.actionProcess = action
end

function XTweenerAction:GetTypeMark()
    return 'XTweenerAction'
end