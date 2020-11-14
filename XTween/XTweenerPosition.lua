---@class XTweenerPosition:XTweener
XTweenerPosition = class(XTweener)


---@param moveType XTweenMoveType
---@param target UnityEngine.Transform
---@param startValue Vector3
---@param endValue Vector3
---@param duration float
---@param playTimes int
---@param pingPong bool
---@param curv XTweenCurvType
function XTweenerPosition:Construction(target, startValue, endValue, duration, playTimes, moveType, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv='..tostring(curv))
        logError(debug.traceback())
    end
    if moveType == XTweenMoveType.AnchoredPosition then
        self.rectTransformTarget = target:GetComponent("RectTransform")
        if not self.rectTransformTarget then
            moveType = XTweenMoveType.LocalPosition
            logError("XTweenerPosition 获取 RectTransform 控件失败，moveType 即将自动转换成 LocalPosition。")
        end
    end

    self:SetTarget(target)
    --self.target = target
    self.startValue = startValue
    self.endValue = endValue
    self.moveType = moveType

    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes
end

function XTweenerPosition:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end

    ---@type Vector3
    local value = self.startValue + (self.endValue - self.startValue) * self.animationProgress
    if self.moveType == XTweenMoveType.LocalPosition then
        self.target.localPosition = value
    elseif self.moveType == XTweenMoveType.WorldPosition then
        self.target.position = value
    elseif self.moveType == XTweenMoveType.AnchoredPosition then
        self.rectTransformTarget.anchoredPosition = value
    end

    self:XTweenerCompleteFinish()


end


function XTweenerPosition:OnPlay()
    if not self.startValue then
        if self.moveType == XTweenMoveType.LocalPosition then
            self.startValue = self.target.localPosition
        elseif self.moveType == XTweenMoveType.WorldPosition then
            self.startValue = self.target.position
        elseif self.moveType == XTweenMoveType.AnchoredPosition then
            self.startValue = self.rectTransformTarget.anchoredPosition
        end
    end
    if not (self.startValue and self.startValue.x and self.startValue.x) then
        logError('XTweenerPosition 参数错误！！！')
        logError(debug.traceback())
    end
end

function XTweenerPosition:GetTypeMark()
    return 'XTweenerPosition'
end