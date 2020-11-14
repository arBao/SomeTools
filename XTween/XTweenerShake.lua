---@class XTweenerShake
XTweenerShake = class()

---@return XTweenerShake
---@param duration float
---@param magnitude float rectTransform的距离
---@param target UnityEngine.Transform
function XTweenerShake:Construction(target, duration, magnitude, delay)
    ---@type userdata
    self.data = nil
    self.delay = delay or 0
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

    --self:SetTarget(target)
    self.target = target
    self.rectTarget = target:GetComponent("RectTransform")

    self.duration = duration
    self.magnitude = magnitude

    return self
end

function XTweenerShake:GetState()
    return self.state
end

function XTweenerShake:SetTweenerData(data)
    self.data = data
end

function XTweenerShake:GetTweenerData()
    return self.data
end

function XTweenerShake:SetDelay(delay)
    self.delay = delay
end

function XTweenerShake:GetDelay()
    return self.delay
end

function XTweenerShake:Play()

    self.oriPos = self.target.position

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

function XTweenerShake:Finish()
    self.state = XTweenerState.End
end

function XTweenerShake:Pause()
    if self.state == XTweenerState.Playing then
        self.state = XTweenerState.Pause
    end
end

function XTweenerShake:Resume()
    if self.state == XTweenerState.Pause then
        self.state = XTweenerState.Playing
    end
end

---@param action Action<XTweenerIF>
function XTweenerShake:SetFinishCallback(action)
    self.finishAction = action
end

---@param action Action<XTweenerIF>
function XTweenerShake:SetStartCallback(action)
    self.startAction = action
end
---@param speed float
function XTweenerShake:SetSpeed(speed)
    self.speed = speed
end

function XTweenerShake:SetControlByParent()
    self.controlByParent = true
end

---@param deltaTime float
function XTweenerShake:Update(deltaTime)
    self.currentTime = self.currentTime + deltaTime * self.speed
    if self.currentTime >= self.delay and self.currentTime < self.delay + self.duration then
        local x = Random.Range(-1, 1) * self.magnitude
        local y = Random.Range(-1, 1) * self.magnitude
        self.rectTarget.anchoredPosition = Vector2.New(x, y)
    elseif self.currentTime >= self.delay + self.duration then
        self.target.position = self.oriPos
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
function XTweenerShake:SetAction(action)
    self.actionProcess = action
end

function XTweenerShake:GetTypeMark()
    return 'XTweenerShake'
end