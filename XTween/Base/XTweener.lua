---@class XTweener
XTweener = class()

function XTweener:_Construction()
    self.data = nil
    self.delay = 0
    ---@type XTweenerState
    self.state = XTweenerState.BeforePlay
    ---@type Action<XTweenerIF>
    self.finishAction = nil
    ---@type Action<XTweenerIF>
    self.startAction = nil
    self.currentTime = 0
    self.timeProgress = 0
    self.speed = 1.0
    self.currentRepeatTimes = 0
    self.controlByParent = false
    ---@type XTweenObserver
    self.observer = nil

    self.duration = 0
    self.animationProgress = 0
    ---@type XTweenCurvType
    self.curvType = XTweenCurvType.Linear
    self.pingPong = false
    self.repeatTimes = 1
    self.beReplaced = false
    ---@type UnityEngine.Transform
    self.target = nil
    self.shouldCallback = true
    self.shouldFinishCallbackWhenDisable = false
    self.shouldNotFinishWhenDisable = false;
    self.gameObjectDisable = false
end

function XTweener:SetShouldFinishCallbackWhenDisable(shouldCallback)
    self.shouldFinishCallbackWhenDisable = shouldCallback
end

function XTweener:SetShouldNotFinishWhenDisable()
    self.shouldNotFinishWhenDisable = true
end

function XTweener:Construction()
    self:_Construction()
end

function XTweener:ConstructionCallBySubClass()
    self:_Construction()
end

function XTweener:GetState()
    return self.state
end

function XTweener:SetTweenerData(data)
    self.data = data
end

function XTweener:GetTweenerData()
    return self.data
end

function XTweener:SetDelay(delay)
    self.delay = delay
end

function XTweener:GetDelay()
    return self.delay
end

function XTweener:SetTarget(target)
    self.target = target

    if not self.observer then
        self:AddObserver()
    end
end

function XTweener:Play()

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

    self:OnPlay()
end

---子类重写这个方法
function XTweener:OnPlay()

end

function XTweener:Finish(shouldCallback)
    self.state = XTweenerState.End
    self.shouldCallback = shouldCallback
end

function XTweener:Pause()
    if self.state == XTweenerState.Playing then
        self.state = XTweenerState.Pause
    end
end

function XTweener:Resume()
    if self.state == XTweenerState.Pause then
        self.state = XTweenerState.Playing
    end
end

function XTweener:SetTime(time)
    self.duration = time
end

---@param action Action<XTweenerIF>
function XTweener:SetFinishCallback(action)
    self.finishAction = action
end

---@param action Action<XTweenerIF>
function XTweener:SetStartCallback(action)
    self.startAction = action
end

function XTweener:_Update(deltaTime)
    self.currentTime = self.currentTime + deltaTime * self.speed
    if self.duration < 0.01 then
        if self.currentTime > self.delay then
            self.timeProgress = 1
        else
            self.timeProgress = 0
        end
    else
        if self.pingPong == true and self.currentTime > self.duration then
            self.timeProgress = (self.currentTime - self.delay - self.duration) / self.duration
            self.timeProgress = 1 - self.timeProgress
        else
            self.timeProgress = (self.currentTime - self.delay) / self.duration
        end
    end

    if self.timeProgress < 0 then
        self.timeProgress = 0
    elseif self.timeProgress > 1 then
        self.timeProgress = 1
    end

    if self.currentTime >= self.delay + self.duration * (self.pingPong and 2 or 1) then
        self.currentRepeatTimes = self.currentRepeatTimes + 1
        if self.currentRepeatTimes >= self.repeatTimes and self.repeatTimes ~= -1 then
            self.state = XTweenerState.End
        else
            self.currentTime = self.delay
        end
    end

    self.animationProgress = XTweenCurv.GetProgress(self.curvType, self.timeProgress, self.tCustomCurv)
end

function XTweener:XTweenerCompleteFinish()
    if self.state == XTweenerState.End then
        self.observer:RemoveTweener(self)
        ----如果gameobject没有隐藏，走回调。如果隐藏了，而且隐藏了应该回调(默认隐藏了不回调),就走回调。
        if self.finishAction 
        and self.shouldCallback == true 
        and
                (self.gameObjectDisable == false or (self.gameObjectDisable == true and self.shouldFinishCallbackWhenDisable == true)) then
            self.finishAction(self)
        end
    end
end

function XTweener:Update(deltaTime)
    self:_Update(deltaTime)
end

function XTweener:UpdateCallBySubClass(deltaTime)
    self:_Update(deltaTime)
end

function XTweener:SetSpeed(speed)
    self.speed = speed
end

function XTweener:SetControlByParent()
    self.controlByParent = true
end

function XTweener:GetControlByParent()
    return self.controlByParent
end

function XTweener:OnEnable()
    --if self.state == XTweenerState.Pause then
    --    self.state = XTweenerState.Playing
    --end
    self.gameObjectDisable = false
end

function XTweener:OnDisable()
    --if self.state == XTweenerState.Playing then
    --    self.state = XTweenerState.Pause
    --end

    -- self.state = XTweenerState.End
    self.gameObjectDisable = true
    if self.shouldNotFinishWhenDisable == true then
        self.state = XTweenerState.End
    end
    
end

function XTweener:OnDestroy()
    self.state = XTweenerState.End
end

function XTweener:BeReplaced()
    self.beReplaced = true
end

---@param curv table  设置自定义的曲线
function XTweener:SetCustomCurv(curv)
    self.tCustomCurv = curv
end

function XTweener:AddObserver()
    ---@type XTweenObserver
    self.observer = LuaBehaviour.Get(self.target.gameObject, XTweenObserver)
    if IsNull(self.observer) then
        self.observer = LuaBehaviour.Add(self.target.gameObject, XTweenObserver)
    end
    self.observer:AddXTweener(self)
end

























