using System.CodeDom;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Character.Abilities
{
	[CreateAssetMenu(menuName = "Abilities/Spawn crate")]
	public class CrateSpawnAbility : Ability
	{
		public LineRenderer drawingPrefab;
		public GameObject cratePrefab;

		private LineRenderer lineRenderer;
		
		public override void OnTrigger(PointerEventData pointerData)
		{
			ResetLineRenderer();
			ResetBounds();
		}

		public override void OnChanneling(PointerEventData pointerData)
		{
			UpdateLine(pointerData.position);
		}

		public override void OnStop(PointerEventData pointerData)
		{
			SpawnCrate();
			lineRenderer.gameObject.SetActive(false);
		}
		
		private void UpdateLine(Vector3 worldPoint)
		{
			AddToLine(worldPoint);
			UpdateBounds(worldPoint);
		}

		private void SpawnCrate()
		{
			var spawnPoint = (min + max) / 2f;
			var scaleFactor = Mathf.Min(max.x - min.x, max.y - min.y);

			var crateInstance = Instantiate(cratePrefab, spawnPoint, Quaternion.identity);
			crateInstance.transform.localScale = Vector3.one * scaleFactor;
			
			var crateRB = crateInstance.GetComponent<Rigidbody2D>();
			crateRB.mass *= scaleFactor;
		}

		private void AddToLine(Vector2 worldPoint)
		{
			lineRenderer.positionCount += 1;
			lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPoint);
		}
		
		private Vector2 min;
		private Vector2 max;

		private void UpdateBounds(Vector2 worldPoint)
		{
			if (worldPoint.x > max.x) {
				max.x = worldPoint.x;
			}

			if (worldPoint.x < min.x) {
				min.x = worldPoint.x;
			}

			if (worldPoint.y > max.y) {
				max.y = worldPoint.y;
			}

			if (worldPoint.y < min.y) {
				min.y = worldPoint.y;
			}
		}

		private void ResetBounds()
		{
			max.x = max.y = float.MinValue;
			min.x = min.y = float.MaxValue;
		}

		private void ResetLineRenderer()
		{
			lineRenderer.gameObject.SetActive(true);
			lineRenderer.positionCount = 0;
		}

		public override void Initialize(GameObject actor)
		{
			base.Initialize(actor);

			lineRenderer = Instantiate(drawingPrefab);
			lineRenderer.gameObject.SetActive(false);
			
			ResetBounds();
		}
	}
}
