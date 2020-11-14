require "ListView/ListView"

ListViewLua = {}
function ListViewLua.Init(
    gameObject,
    scrollstyle,
    spacingForRowCallback,
    countOfRowsCallback,
    getCellCallback,
    cellForRow
)
    local listview = LuaBehaviour.Add(gameObject,ListView)
    listview:Init(
        scrollstyle,
        spacingForRowCallback,
        countOfRowsCallback,
        getCellCallback,
        cellForRow
    )


    return listview
end
