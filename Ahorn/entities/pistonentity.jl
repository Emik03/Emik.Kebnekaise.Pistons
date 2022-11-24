module KebnekaisePistonEntity
using ..Ahorn, Maple

@pardef PistonEntity(
    x1::Integer,
    y1::Integer,
    width::Integer=8,
    height::Integer=8,
    time::Float32=1,
    tiletype::String="3",
    ease::String="Linear") = Entity("KebnekaiseHelper/PistonEntity",
        x = x1,
        y = y1,
        nodes = Tuple{Int, Int}[(x1 + 8,y1)],
        width = width,
        height = height,
        time = time,
        tiletype = tiletype,
        ease = ease)

const placements = Ahorn.PlacementDict(
    "Piston Entity (Kebnekaise)" => Ahorn.EntityPlacement(
        PistonEntity,
        "rectangle",
        Dict{String, Any}(),
        Ahorn.tileEntityFinalizer)
)

Ahorn.editingOptions(entity::PistonEntity) = Dict{String, Any}(
    "ease" => String[
        "Linear",
        "SineIn",
        "SineOut",
        "SineInOut",
        "QuadIn",
        "QuadOut",
        "QuadInOut",
        "CubeIn",
        "CubeOut",
        "CubeInOut",
        "QuintIn",
        "QuintOut",
        "QuintInOut",
        "BackIn",
        "BackOut",
        "BackInOut",
        "ExpoIn",
        "ExpoOut",
        "ExpoInOut",
        "BigBackIn",
        "BigBackOut",
        "BigBackInOut",
        "ElasticIn",
        "ElasticOut",
        "ElasticInOut",
        "BounceIn",
        "BounceOut",
        "BounceInOut"]
)

Ahorn.resizable(entity::PistonEntity) = true, true
Ahorn.minimumSize(entity::PistonEntity) = 8, 8
Ahorn.nodeLimits(entity::PistonEntity) = 1, 1

function Ahorn.selection(entity::PistonEntity)
    x, y = Ahorn.position(entity)
    nx, ny = Int.(entity.data["nodes"][1])

    width = Int(get(entity.data, "width", 8))
    height = Int(get(entity.data, "height", 8))

    return [Ahorn.Rectangle(x, y, width, height), Ahorn.Rectangle(nx, ny, width, height)]
end

function Ahorn.renderAbs(ctx::Ahorn.Cairo.CairoContext, entity::PistonEntity, room::Maple.Room)
    tiletype = get(entity.data, "tiletype", "3")[1]
    Ahorn.drawTileEntity(ctx, room, entity, material = tiletype, blendIn = false)
end

function Ahorn.renderSelectedAbs(ctx::Ahorn.Cairo.CairoContext, entity::PistonEntity, room::Maple.Room)
    x, y = Ahorn.position(entity)
    nodes = get(entity.data, "nodes", ())

    width = Int(get(entity.data, "width", 8))
    height = Int(get(entity.data, "height", 8))

    if !isempty(nodes)
        nx, ny = Int.(nodes[1])
        cox, coy = floor(Int, width / 2), floor(Int, height / 2)
        tiletype = get(entity.data, "tiletype", "3")
        fakeTiles = Ahorn.createFakeTiles(room, nx, ny, width, height, tiletype[1], blendIn = false)
        Ahorn.drawFakeTiles(ctx, room, fakeTiles, room.objTiles, true, nx, ny, clipEdges = true)
        Ahorn.drawArrow(ctx, x + cox, y + coy, nx + cox, ny + coy, Ahorn.colors.selection_selected_fc, headLength = 6)
    end
end

end
