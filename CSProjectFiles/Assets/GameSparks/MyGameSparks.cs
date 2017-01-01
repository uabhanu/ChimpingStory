using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
}
	
	
	
namespace GameSparks.Api.Requests{
	
	public class LeaderboardDataRequest_C : GSTypedRequest<LeaderboardDataRequest_C,LeaderboardDataResponse_C>
	{
		public LeaderboardDataRequest_C() : base("LeaderboardDataRequest"){
			request.AddString("leaderboardShortCode", "C");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LeaderboardDataResponse_C (response);
		}		
		
		/// <summary>
		/// The challenge instance to get the leaderboard data for
		/// </summary>
		public LeaderboardDataRequest_C SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public LeaderboardDataRequest_C SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_C SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public LeaderboardDataRequest_C SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public LeaderboardDataRequest_C SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// The offset into the set of leaderboards returned
		/// </summary>
		public LeaderboardDataRequest_C SetOffset( long offset )
		{
			request.AddNumber("offset", offset);
			return this;
		}
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_C SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public LeaderboardDataRequest_C SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public LeaderboardDataRequest_C SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
		
	}

	public class AroundMeLeaderboardRequest_C : GSTypedRequest<AroundMeLeaderboardRequest_C,AroundMeLeaderboardResponse_C>
	{
		public AroundMeLeaderboardRequest_C() : base("AroundMeLeaderboardRequest"){
			request.AddString("leaderboardShortCode", "C");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new AroundMeLeaderboardResponse_C (response);
		}		
		
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public AroundMeLeaderboardRequest_C SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_C SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public AroundMeLeaderboardRequest_C SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public AroundMeLeaderboardRequest_C SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_C SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_C SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_C SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
	}
}

namespace GameSparks.Api.Responses{
	
	public class _LeaderboardEntry_C : LeaderboardDataResponse._LeaderboardData{
		public _LeaderboardEntry_C(GSData data) : base(data){}
	}
	
	public class LeaderboardDataResponse_C : LeaderboardDataResponse
	{
		public LeaderboardDataResponse_C(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_C> Data_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_C> First_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_C> Last_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
	}
	
	public class AroundMeLeaderboardResponse_C : AroundMeLeaderboardResponse
	{
		public AroundMeLeaderboardResponse_C(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_C> Data_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_C> First_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_C> Last_C{
			get{return new GSEnumerable<_LeaderboardEntry_C>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_C(data);});}
		}
	}
}	

namespace GameSparks.Api.Messages {


}
