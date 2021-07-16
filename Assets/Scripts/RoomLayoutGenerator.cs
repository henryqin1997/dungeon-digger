using System.Collections.Generic;
using System.Diagnostics;
using System;

public class RoomConfig
{
    public struct Coordinates
    {
        public int x; // x-offset from the "initial" room (LEFT/RIGHT => -/+)
        public int y; // y-offset from the "initial" room (DOWN/UP    => -/+)
    }
    public enum DoorDirections
    {
        LEFT  = 0b0001,
        RIGHT = 0b0010,
        UP    = 0b0100,
        DOWN  = 0b1000
    }

    public Coordinates position; // offset from the "initial" room
    public DoorDirections doors; // set of doors ('(doors & <DIR>) != 0' indicates presence of door
    public string type;          // "initial", "normal", or "boss"
}

static class RoomLayoutGenerator
{
    // Randomly generate a list of square rooms that are completely connected
    // by doors, where each room is reachable from every other room.  All rooms
    // but two are marked with type "normal": the "initial" room, and the
    // "boss" room.  The specified 'minDistanceToBoss' corresponds to the
    // minimum number of doors that must be opened in order to travel from the
    // "initial" room to the "boss" room.
    public static List<RoomConfig> GenerateRoomLayout(int minDistanceToBoss)
    {
        Debug.Assert(minDistanceToBoss > 0);

        Random rg = new Random();
        SortedDictionary<int, List<RoomConfig>> frontier = new SortedDictionary<int, List<RoomConfig>>();
        Dictionary<int, Dictionary<int, int>> roomDepth = new Dictionary<int, Dictionary<int, int>>();
        Dictionary<int, Dictionary<int, RoomConfig>> rooms = new Dictionary<int, Dictionary<int, RoomConfig>>();

        RoomConfig initialRoom = new RoomConfig();
        initialRoom.position.x = 0;
        initialRoom.position.y = 0;
        initialRoom.doors = 0;
        initialRoom.type = "initial";
        InsertIntoRoomDict(initialRoom, initialRoom, rooms);
        InsertIntoRoomDict(initialRoom, 0, roomDepth);
        PushRoom(frontier, rg, initialRoom);

        while (frontier.Count > 0)
        {
            RoomConfig room = PopRoom(frontier);
            int depth = roomDepth[room.position.x][room.position.y];
            if (depth == minDistanceToBoss)
            {
                room.type = "boss";
                break;
            }

            foreach (RoomConfig.DoorDirections wall in SampleWalls(room, rg))
            {
                if (GetNeighbor(room, wall, rooms, out var neighbor, out var neighborWall))
                {
                    if (ShouldBreakWall(rg))
                    {
                        BreakWall(room, wall, neighbor, neighborWall, roomDepth, rooms);
                    }
                    continue;
                }
                room.doors |= wall;
                InsertIntoRoomDict(neighbor, neighbor, rooms);
                InsertIntoRoomDict(neighbor,
                                   roomDepth[room.position.x][room.position.y] + 1,
                                   roomDepth);
                PushRoom(frontier, rg, neighbor);
            }
        }

        List<RoomConfig> result = new List<RoomConfig>();
        foreach (var yRooms in rooms)
        {
            foreach (var entry in yRooms.Value)
            {
                result.Add(entry.Value);
            }

        }
        return result;
    }

    private static void InsertIntoRoomDict<T>(in RoomConfig room,
                                              T item,
                                              Dictionary<int, Dictionary<int, T>> roomDict)
    {
        if (!roomDict.TryGetValue(room.position.x, out var xDict))
        {
            xDict = roomDict[room.position.x] = new Dictionary<int, T>();
        }
        xDict[room.position.y] = item;
    }

    private static void BreakWall(RoomConfig room,
                                  RoomConfig.DoorDirections wall,
                                  RoomConfig neighbor,
                                  RoomConfig.DoorDirections neighborWall,
                                  Dictionary<int, Dictionary<int, int>> roomDepth,
                                  Dictionary<int, Dictionary<int, RoomConfig>> rooms)
    {
        room.doors |= wall;
        neighbor.doors |= neighborWall;

        int depth = roomDepth[room.position.x][room.position.y];
        int neighborDepth = roomDepth[neighbor.position.x][neighbor.position.y];
        RoomConfig shallowerRoom = (depth < neighborDepth) ? room : neighbor;
        CorrectDepth(shallowerRoom, roomDepth, rooms);
    }

    private static void CorrectDepth(RoomConfig room,
                                     Dictionary<int, Dictionary<int, int>> roomDepth,
                                     Dictionary<int, Dictionary<int, RoomConfig>> rooms)
    {
        int depth = roomDepth[room.position.x][room.position.y];

        int[] STEPS = new int[] { 0, 1, 0, -1, 0 };
        RoomConfig.DoorDirections[] DIRS = new RoomConfig.DoorDirections[]
        {
                RoomConfig.DoorDirections.UP,
                RoomConfig.DoorDirections.RIGHT,
                RoomConfig.DoorDirections.DOWN,
                RoomConfig.DoorDirections.LEFT
        };
        for (int i = 0; i < 4; ++i)
        {
            RoomConfig.DoorDirections door = DIRS[i];
            if ((room.doors & door) == 0)
            {
                continue;
            }
            int neighborX = room.position.x + STEPS[i];
            int neighborY = room.position.y + STEPS[i + 1];
            int neighborDepth = roomDepth[neighborX][neighborY];
            if (neighborDepth <= (depth + 1))
            {
                continue;
            }
            roomDepth[neighborX][neighborY] = depth + 1;
            CorrectDepth(rooms[neighborX][neighborY], roomDepth, rooms);
        }
    }

    private static bool ShouldBreakWall(Random rg)
    {
        return rg.Next(2) != 0;
    }

    private static bool GetNeighbor(RoomConfig room,
                                    RoomConfig.DoorDirections wall,
                                    Dictionary<int, Dictionary<int, RoomConfig>> rooms,
                                    out RoomConfig neighbor,
                                    out RoomConfig.DoorDirections neighborWall)
    {
        int x = room.position.x;
        int y = room.position.y;
        switch (wall)
        {
            case RoomConfig.DoorDirections.LEFT:
                --x;
                neighborWall = RoomConfig.DoorDirections.RIGHT;
                break;
            case RoomConfig.DoorDirections.RIGHT:
                ++x;
                neighborWall = RoomConfig.DoorDirections.LEFT;
                break;
            case RoomConfig.DoorDirections.UP:
                ++y;
                neighborWall = RoomConfig.DoorDirections.DOWN;
                break;
            case RoomConfig.DoorDirections.DOWN:
                --y;
                neighborWall = RoomConfig.DoorDirections.UP;
                break;
            default:
                Debug.Fail("Invalid RoomConfig.DoorDirections: " + wall.ToString());
                neighborWall = 0;
                break;
        }
        if (rooms.TryGetValue(x, out var xRooms) &&
            xRooms.TryGetValue(y, out neighbor))
        {
            return true;
        }

        neighbor = new RoomConfig();
        neighbor.position.x = x;
        neighbor.position.y = y;
        neighbor.doors = neighborWall;
        neighbor.type = "normal";

        return false;
    }

    private static List<RoomConfig.DoorDirections> SampleWalls(RoomConfig room, Random rg)
    {
        List<RoomConfig.DoorDirections> walls = new List<RoomConfig.DoorDirections>();
        foreach (RoomConfig.DoorDirections wall in Enum.GetValues(typeof(RoomConfig.DoorDirections)))
        {
            if ((room.doors & wall) == 0)
            {
                walls.Add(wall);
            }
        }

        Shuffle(walls, rg);

        int keepWalls = rg.Next(1, walls.Count);
        int removeWalls = walls.Count - keepWalls;

        walls.RemoveRange(keepWalls, removeWalls);

        return walls;
    }

    public static void Shuffle<T>(this IList<T> list, Random rg)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rg.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private static void PushRoom(SortedDictionary<int, List<RoomConfig>> frontier,
                                 Random rg,
                                 in RoomConfig room)
    {
        int priority = rg.Next();
        if (!frontier.TryGetValue(priority, out var matches))
        {
            matches = frontier[priority] = new List<RoomConfig>();
        }
        matches.Add(room);
    }

    private static RoomConfig PopRoom(SortedDictionary<int, List<RoomConfig>> frontier)
    {
        var enumerator = frontier.GetEnumerator();
        bool successfulAdvance = enumerator.MoveNext();
        Debug.Assert(successfulAdvance);
        int minPriority = enumerator.Current.Key;
        List<RoomConfig> matches = frontier[minPriority];
        int last = matches.Count - 1;
        RoomConfig room = matches[last];
        if (matches.Count == 1)
        {
            frontier.Remove(minPriority);
        }
        else
        {
            matches.RemoveAt(last);
        }
        return room;
    }
}
