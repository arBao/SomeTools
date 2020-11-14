---@class XTweenerRotaion:XTweener
XTweenerRotaion = class(XTweener)

function XTweenerRotaion:Construction(rotationType, target, startValue, endValue, duration, playTimes, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv='..tostring(curv))
        logError(debug.traceback())
    end
    self:SetTarget(target)
    --self.target = target
    self.startValue = startValue
    self.endValue = endValue
    self.rotationType = rotationType

    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes
end

function XTweenerRotaion:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end

    ---@type Vector3
    local value = Vector3.Lerp(self.startValue, self.endValue, self.animationProgress)
    if self.rotationType == XTweenRotationType.LocalRotation then
        self.target.localRotation = Quaternion.Euler(value.x,value.y,value.z)
    elseif self.rotationType == XTweenRotationType.WorldRotation then
        self.target.rotation = Quaternion.Euler(value.x,value.y,value.z)
    end

    self:XTweenerCompleteFinish()

end



function XTweenerRotaion:OnPlay()
    if not self.startValue then
        if self.rotationType == XTweenRotationType.LocalRotation then
            self.startValue = self.target.localEulerAngles
        elseif self.rotationType == XTweenRotationType.WorldRotation then
            self.startValue = self.target.eulerAngles
        end
    end
end


function XTweenerRotaion:GetTypeMark()
    return 'XTweenerRotaion'
end