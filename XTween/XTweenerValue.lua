---@class XTweenerValue:XTweener
XTweenerValue = class(XTweener)

---@param moveType XTweenMoveType
---@param target UnityEngine.Transform
---@param startValue Vector3
---@param endValue Vector3
---@param duration float
---@param playTimes int
---@param pingPong bool
---@param curv XTweenCurvType
function XTweenerValue:Construction(target, startValue, endValue, duration,valueChangeCallback, playTimes, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv='..tostring(curv))
        logError(debug.traceback())
    end
    -- if moveType == XTweenMoveType.AnchoredPosition then
    --     self.rectTransformTarget = target:GetComponent(typeof(RectTransform))
    --     if not self.rectTransformTarget then
    --         moveType = XTweenMoveType.LocalPosition
    --         logError("XTweenerValue 获取 RectTransform 控件失败，moveType 即将自动转换成 LocalPosition。")
    --     end
    -- end

    self.valueChangeCallback = valueChangeCallback

    self:SetTarget(target)
    --self.target = target
    self.startValue = startValue
    self.endValue = endValue
    -- self.moveType = moveType

    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes
end

function XTweenerValue:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end

    ---@type Vector3
    local value = self.startValue + (self.endValue - self.startValue) * self.animationProgress
    -- if self.moveType == XTweenValueType.Slider then
    --     self.target.value = value
    -- end

    self.valueChangeCallback(value)
    self:XTweenerCompleteFinish()
end


function XTweenerValue:OnPlay()
    if not self.startValue then
        self.startValue = self.target.value
    end
end


function XTweenerValue:GetTypeMark()
    return 'XTweenerValue'
end